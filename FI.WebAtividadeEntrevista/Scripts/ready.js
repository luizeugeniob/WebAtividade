$(document).ready(function () {
    $(".cep").inputmask("mask", { "mask": "99999-999" });
    $(".cpf").inputmask("mask", { "mask": "999.999.999-99" }, { reverse: true });
    $(".telefone").inputmask("mask", { "mask": "(99) 9999-99999" });
});