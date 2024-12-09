    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("CustCountry").value = "Canada";
    let country_code = document.getElementById("CustCountry").value == 'US' ? "US" : "CA";

    function fetchProv(country_code) {
        console.log(country_code, "cc");
    let apiUrlProv = `https://public.opendatasoft.com/api/explore/v2.1/catalog/datasets/geonames-postal-code/records?group_by=admin_code1&limit=40&refine=country_code%3A%22${country_code}%22`
    fetch(apiUrlProv)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
    return response.json();
                })
                .then(data => {
        console.log("data", data.results);
                    if (data.results && data.results.length > 0) {
        let htmlCode = "";
                        data.results.forEach(e => { if (e.admin_code1 != null) htmlCode += `<option>${e.admin_code1}</option>` })
    document.getElementById('CustProv').innerHTML = htmlCode;

                    } else {
        alert("No details found for the entered postal code.");
                    }
                })
                .catch(error => {
        console.error("Error fetching postal code details:", error);
    alert("Failed to validate postal code. Please try again later.");
                });
        }

    fetchProv(country_code);

        document.getElementById("CustCountry").addEventListener('change', (value) => {
        let cc = document.getElementById("CustCountry").value == 'US' ? "US" : "CA";
    fetchProv(cc);
        });

    const container = document.querySelector("#collapseOne");
    const inputs = container.querySelectorAll("input");


    console.log(inputs);
    const submitButton = container.querySelector(".btn-primary");

    // Validation rules for each input
    const validationRules = {
        CustFirstName: (value) => value.trim().length > 2, // Required
            CustLastName: (value) => value.trim().length > 2, // Required
            CustAddress: (value) => value.trim().length > 5, // Required
            CustCity: (value) => value.trim() !== "", // Required
            CustProv: (value) => value.trim() !== "", // Required
            CustPostal: (value) => validatePostalCode(value).length>0, // Alphanumeric, 5-10 chars
            CustCountry: (value) => value.trim() !== "" // Required
        };

    function validatePostalCode(postalCode) {
            const canadaRegex = /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/; // Canada: A1A 1A1 or A1A1A1
    const usRegex = /^\d{5}(-\d{4})?$/; // US: 12345 or 12345-6789

    if (canadaRegex.test(postalCode)) {
                return "CA"; // Canada
            } else if (usRegex.test(postalCode)) {
                return "US"; // United States
            } else {
                return ""; // Invalid
            }
        }

    document.getElementById("CustPostal").addEventListener("blur", function () {
        let postalCode = this.value.trim();
    const countryCode = validatePostalCode(postalCode);

    if (countryCode.length == 0) {
        alert("Invalid postal code format. Please enter a valid US or Canadian postal code.");
    return;
            }

    if (countryCode == 'CA') {
        postalCode = postalCode.substr(0, 3);
            }

    console.log(postalCode, countryCode);

    const apiUrl = `https://public.opendatasoft.com/api/explore/v2.1/catalog/datasets/geonames-postal-code/records?where=postal_code%3D"${encodeURIComponent(postalCode)}"&limit=20&refine=country_code%3A"${countryCode}"`;

    fetch(apiUrl)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }
    return response.json();
                })
                .then(data => {
        console.log("data", data.results);
                    if (data.results && data.results.length > 0) {


                        const result = data.results[0];

    // Fill the fields
    document.getElementById("CustCountry").value = countryCode === "US" ? "US" : "Canada";
    validateInput(document.getElementById("CustCountry"));
    document.getElementById("CustProv").value = result.admin_code1 || ""; // Province/State
    validateInput(document.getElementById("CustProv"));
    document.getElementById("CustCity").value = result.admin_name2 || ""; // City
    validateInput(document.getElementById("CustCity"));
                    } else {
        alert("No details found for the entered postal code.");
                    }
                })
                .catch(error => {
        console.error("Error fetching postal code details:", error);
    alert("Failed to validate postal code. Please try again later.");
                });
        });



    // Validate a single input
    function validateInput(input) {
            const rule = validationRules[input.getAttribute("id")];
    if (rule) {
                const isValid = rule(input.value);
    if(isValid){
        input.classList.add("is-valid", !isValid);
    input.classList.remove("is-invalid", !isValid);
                }
    else{
        input.classList.add("is-invalid", !isValid);
    input.classList.remove("is-valid", !isValid);
                }
    return isValid;
            }

    return true;
        }

    // Validate all inputs
    function validateForm() {
        let isFormValid = true;
            inputs.forEach((input) => {
                const isValid = validateInput(input);
    if (!isValid) {
        isFormValid = false;
                }
            });
    submitButton.disabled = !isFormValid;
    return isFormValid;
        }

        // Event listeners for real-time validation
        inputs.forEach((input) => {
        input.addEventListener("input", () => {
            validateInput(input);
            if (validateForm()) {
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.remove("disabled");
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.remove("collapsed");
                submitButton.classList.remove("disabled");
            }
            else if (submitButton.classList.contains("disabled")) {
                submitButton.classList.add("disabled");
            }
        });
        });

        // Submit button click handler
        submitButton.addEventListener("click", (e) => {
        e.preventDefault();
            if (validateForm()) {
                moveToNext();
            }
        });




        function formatPhoneNumber(input) {
            const cleaned = input.replace(/\D/g, ''); // Remove non-digit characters
            const limited = cleaned.substring(0, 10); // Limit to 10 digits
            const match = limited.match(/^(\d{0,3})(\d{0,3})(\d{0,4})$/);
            if (!match) return input;

            // Format the phone number
            return !match[2]
                ? `(${match[1]}`
                : `(${match[1]}) ${match[2]}${match[3] ? ' - ' + match[3] : ''}`;
        }

        function validatePhoneNumber(input) {
            const phoneRegex = /^\(\d{3}\) \d{3} - \d{4}$/;
            return phoneRegex.test(input);
        }

        function validateEmail(email) {
            const emailRegex = /^^[a-zA-Z0-9._%+-]{2,}@[a-zA-Z0-9.-]{2,}\.[a-zA-Z]{2,}$/;
            return emailRegex.test(email);
        }

        function removeIncompletePhoneNumber(element) {
            if (element.value.endsWith("(")) {
                element.value = ""; // Clear the incomplete opening parenthesis
            }
        }

        // Event Listeners
        document.getElementById("CustHomePhone").addEventListener("input", function () {
            this.value = formatPhoneNumber(this.value);
        });

        document.getElementById("CustHomePhone").addEventListener("blur", function () {
            removeIncompletePhoneNumber(this);
            const errorElement = document.getElementById("CustHomePhone").parentElement.getElementsByTagName("span")[0];
            if (!validatePhoneNumber(this.value) && this.value !== "") {
                errorElement.textContent = "Invalid phone number format. Use (xxx) xxx - xxxx.";
            } else {
                errorElement.textContent = "";
            }
        });

        document.getElementById("CustBusPhone").addEventListener("input", function () {
            this.value = formatPhoneNumber(this.value);
        });

        document.getElementById("CustBusPhone").addEventListener("blur", function () {
            removeIncompletePhoneNumber(this);
            const errorElement = document.getElementById("CustBusPhone").parentElement.getElementsByTagName("span")[0];
            if (!validatePhoneNumber(this.value) && this.value !== "") {
                errorElement.textContent = "Invalid phone number format. Use (xxx) xxx - xxxx.";
            } else {
                errorElement.textContent = "";
            }
        });

        document.getElementById("CustEmail").addEventListener("blur", function () {
            const errorElement = document.getElementById("CustEmail").parentElement.getElementsByTagName("span")[0];
            if (!validateEmail(this.value)) {
                errorElement.textContent = "Invalid email address.";
            } else {
                errorElement.textContent = "";
            }
        });
    });

    // Example moveToNext function
    function moveToNext() {
        document.getElementById("collapseTwo").classList.add("show");
        document.getElementById("collapseOne").classList.remove("show");
        document.getElementById("headingOne").getElementsByTagName("button")[0].classList.add("collapsed");
        if (document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.contains("collapsed")) {
            document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.remove("collapsed")
        }
    }

