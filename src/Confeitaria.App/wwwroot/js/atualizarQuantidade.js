function increment(button) {
    var input = button.previousElementSibling;
    input.value = parseInt(input.value) + 1;
    input.form.submit();
}

function decrement(button) {
    var input = button.nextElementSibling;
    if (parseInt(input.value) > 1) {
        input.value = parseInt(input.value) - 1;
        input.form.submit();
    }
}