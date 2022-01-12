using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public IEnumerable<Object> GetUserData(LoginVM loginVM)
        {
            var emp = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
            var query = myContext.AccountRoles.Where(a => a.NIK == emp.NIK).Select(a => a.Role.Name).ToList();
            return query;
        }

        public int Login(LoginVM loginVM)
        {
            var employeeData = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
            if (employeeData != null)
            {
                var accountData = myContext.Accounts.Where(a => a.NIK == employeeData.NIK).FirstOrDefault();
                if (BCrypt.Net.BCrypt.Verify(loginVM.Password, accountData.Password))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            // Check if email exist in system
            var emp = myContext.Employees.SingleOrDefault(e => e.Email == forgotPasswordVM.Email);
            if (emp == null)
            {
                return 0;
            }
            // Generate OTP
            Random rnd = new Random();
            int otp = rnd.Next(100000, 999999);
            //expired token
            DateTime exp = DateTime.Now.AddMinutes(15);
            // Set OTP, Expired token, Is used
            var acc = myContext.Accounts.SingleOrDefault(e => e.NIK == emp.NIK);
            acc.OTP = otp;
            acc.ExpiredToken = exp;
            acc.IsUsed = false;

            // send email
            SendMail(otp, emp.FirstName, forgotPasswordVM.Email, exp);
            // Update
            myContext.Entry(acc).State = EntityState.Modified;
            int respond = myContext.SaveChanges();
            return respond;
        }

        public void SendMail(int otp, string name, string email, DateTime exp)
        {
            string to = email; //To address    
            string from = "mccreg61net@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = $"We have requested password change, enter this OTP code : {otp} to the web page" +
                $" You can change your password before : {exp}";
            message.Subject = $"Hi {name}! Here is your OTP number";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587); //Gmail smtp    

            // Credential set
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("mccreg61net@gmail.com", "61mccregnet");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int ChangePassword(ForgotPasswordVM forgotPasswordVM)
        {
            // Check if OTP exist in system
            var acc = myContext.Accounts.SingleOrDefault(a => a.OTP == forgotPasswordVM.OTP);
            DateTime currentTime = DateTime.Now;
            if (acc!=null)
            {
                if (currentTime > acc.ExpiredToken )
                {
                    return -1;
                }
                else
                {
                    if (acc.IsUsed == true)
                    {
                        return -2;
                    }
                }
            }
            else
            {
                return 0;
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(forgotPasswordVM.Password);
            acc.Password = hash;
            acc.IsUsed = true;

            myContext.Entry(acc).State = EntityState.Modified;
            int respond = myContext.SaveChanges();
            return respond;
        }

        
    }
}
