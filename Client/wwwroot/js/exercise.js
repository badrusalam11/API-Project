const animals = [
    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
]
let onlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species==="cat") {
        onlyCat.push(animals[i]);
    }
    else if (animals[i].species === "snail") {
        animals[i].kelas.name = "Non Mamalia";
    }
}


