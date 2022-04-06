$(document).ready(function () {
    save();
});

function save() {
    var $input = $("#input");
    var $quantity = $("#quantity");

    $quantity.click(function () {
        var input = document.getElementById("#quantity");
        console.log(input);
        console.log("rudy");
    });
};

save();
console.log('artur');