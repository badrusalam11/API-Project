
//$.ajax({
//    url: "https://localhost:44378/API/Employees"
//}).done((result) => {
//    var text = "";
//    $.each(result, function (key, val) {
//        text += `
//            <tr>
//            <td>${key + 1}</td>
//            <td>${val.firstName}</td>
//            <td>${val.lastName}</td>
//            <td>${val.birthDate}</td>
//            </tr>
//            `;
//    });
//    console.log(result);
//    $(".employeeTable").html(text);
//}).fail((error) => {
//    console.log(error);
//});

    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon"
    }).done((result) => {
        console.log(result);
        var text = "";
        $.each(result.results, function (key, val) {
            text += `
            <tr>
            <td>${key + 1}</td>
            <td id="nama" >${val.name}</td>
            <td><button type="button" onclick="getDetails('${val.url}')"
            class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">Detail</button></td>
            </tr>
            `;
        });
        //console.log(text);
        $(".pokeTable").html(text);
    }).fail((error) => {
        console.log(error);
    });

function getDetails(link) {
    $.ajax({
        url: link
    }).done((result) => {
        console.log(result);
        var text = "";
        text = `
        <div class="container">
        <img src="${result.sprites.other.dream_world.front_default}" alt="Alternate Text" />
        </br>
        <h1 class="text-capitalize">${result.name}</h1>
        </br>
        <h4 style="background-color:lightgreen"> Ability : </h4>
        
        `;
        $.each(result.abilities, function (key, val) {
            text += `
            </br>
            ${val.ability.name}
            `;
        });
        text += `</br>
        </br>
        <h4 style="background-color:gold">
        Stats :
        </h4>    `;
        $.each(result.stats, function (key, val) {
            text += `
            </br>
            ${val.stat.name}
<div class="progress">
  <div class="progress-bar" role="progressbar" style="width:${val.base_stat}%" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
</div>
            `;
        });
        text += `</br>
        <h4 style="background-color:salmon">
        Type :
        </h4>    `;

        $.each(result.types, function (key, val) {
            text +=
                typeColor(val.type.name) +
                `</div>`;
        });

        
        $(".modal-body").html(text);
    }).fail((error) => {
        console.log(error);
    });

}

function typeColor(type) {
    switch (type) {
        case "bug":
            return `<span style="background-color:#a8b820">${type}</span>`
            break;
        case "dark":
            return `<span style="background-color:#705848">${type}</span>`
            break;
        case "dragon":
            return `<span style="background-color:#7038f8">${type}</span>`
            break;
        case "electric":
            return `<span style="background-color:#f8d030">${type}</span>`
            break;
        case "fairy":
            return `<span style="background-color:#ee99ac">${type}</span>`
            break;
        case "fighting":
            return `<span style="background-color:#c03028">${type}</span>`
            break;
        case "fire":
            return `<span style="background-color:#f08030">${type}</span>`
            break;
        case "flying":
            return `<span style="background-color:#a890f0">${type}</span>`
            break;
        case "ghost":
            return `<span style="background-color:#705898">${type}</span>`
            break;
        case "grass":
            return `<span style="background-color:#78c850">${type}</span>`
            break;
        case "ground":
            return `<span style="background-color:#e0c068">${type}</span>`
            break;
        case "ice":
            return `<span style="background-color:#98d8d8">${type}</span>`
            break;
        case "normal":
            return `<span style="background-color:#a8a878">${type}</span>`
            break;
        case "normal":
            return `<span style="background-color:#a8a878">${type}</span>`
            break;
        case "normal":
            return `<span style="background-color:#a8a878">${type}</span>`
            break;
        case "normal":
            return `<span style="background-color:#a8a878">${type}</span>`
            break;
        case "poison":
            return `<span style="background-color:#a040a0">${type}</span>`
            break;
        case "psychic":
            return `<span style="background-color:#f85888">${type}</span>`
            break;
        case "rock":
            return `<span style="background-color:#b8a038">${type}</span>`
            break;
        case "steel":
            return `<span style="background-color:#b8b8d0">${type}</span>`
            break;
        case "water":
            return `<span style="background-color:#6890f0">${type}</span>`
            break;

        default:
            return `<span>${type}</span>`
            break;
    }
}

$(document).ready(function () {
    $('#PokemonTable').DataTable();
});


