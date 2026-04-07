function formatBrazilianPhone(value) {
    const digits = value.replace(/\D/g, "").slice(0, 11);

    if (digits.length <= 2) {
        return digits.length ? `(${digits}` : "";
    }

    const areaCode = digits.slice(0, 2);
    const localNumber = digits.slice(2);

    if (digits.length <= 10) {
        const firstPart = localNumber.slice(0, 4);
        const secondPart = localNumber.slice(4, 8);
        return secondPart
            ? `(${areaCode})${firstPart}-${secondPart}`
            : `(${areaCode})${firstPart}`;
    }

    const firstPart = localNumber.slice(0, 5);
    const secondPart = localNumber.slice(5, 9);
    return secondPart
        ? `(${areaCode})${firstPart}-${secondPart}`
        : `(${areaCode})${firstPart}`;
}

document.addEventListener("DOMContentLoaded", function () {
    const phoneInputs = document.querySelectorAll("[data-phone-mask]");

    phoneInputs.forEach(function (input) {
        input.addEventListener("input", function (event) {
            event.target.value = formatBrazilianPhone(event.target.value);
        });

        input.value = formatBrazilianPhone(input.value);
    });
});
