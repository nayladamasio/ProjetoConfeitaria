function increment() {
    var input = document.querySelector("input[name='quantidade']");
    input.value = parseInt(input.value) + 1;
    input.form.submit();
}

function decrement() {
    var input = document.querySelector("input[name='quantidade']");
    if (input.value > 1) {
        input.value = parseInt(input.value) - 1;
        input.form.submit();
    }
}