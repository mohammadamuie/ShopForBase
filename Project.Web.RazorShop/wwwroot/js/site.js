function NumberFormat(input) {
    //return input.toLocaleString();
    input = input.toString();
    return input.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}