document.addEventListener("DOMContentLoaded", () => {
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
    const inputs = [...container.querySelectorAll("input"), document.getElementById("CustProv")];


    console.log(inputs);
    const moveToContactButton = container.querySelector(".btn-primary");

    // Validation rules for each input
    const validationRules = {
        CustFirstName: (value) => value.trim().length > 2, // Required
        CustLastName: (value) => value.trim().length > 2, // Required
        CustAddress: (value) => value.trim().length > 5, // Required
        CustCity: (value) => value.trim() !== "", // Required
        CustProv: (value) => value.trim() !== "", // Required
        CustPostal: (value) => validatePostalCode(value).length > 0, // Alphanumeric, 5-10 chars
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
        fetchProv(countryCode);
        if (countryCode.length == 0) {
            alert("Invalid postal code format. Please enter a valid US or Canadian postal code.");
            return;
        }

        if (countryCode == 'CA') {
            postalCode = String(postalCode.substr(0, 3)).toUpperCase();
        }

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

                    validateForm();
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
            console.log(isValid);
            if (isValid) {
                input.classList.add("is-valid", !isValid);
                input.classList.remove("is-invalid", !isValid);
            }
            else {
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
        !isFormValid && moveToContactButton.classList.add('disabled');
        isFormValid && moveToContactButton.classList.remove('disabled');
        return isFormValid;
    }

    // Event listeners for real-time validation
    inputs.forEach((input) => {
        input.addEventListener("change", () => {
            validateInput(input);
            if (validateForm()) {
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.remove("disabled");
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.remove("collapsed");
                validateAll();
                moveToContactButton.classList.remove("disabled");
            }
            else {
                !moveToContactButton.classList.contains("disabled") && moveToContactButton.classList.add("disabled");
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.add("disabled");
                document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.add("collapsed");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("disabled");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("collapsed");
            }
        });
    });

    // Submit button click handler
    moveToContactButton.addEventListener("click", (e) => {
        e.preventDefault();
        if (validateForm()) {
            moveToNext();
        }
    });



    const homePhone = document.getElementById("CustHomePhone");
    const busPhone = document.getElementById("CustBusPhone");
    const email = document.getElementById("CustEmail");
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
    [homePhone, busPhone].forEach(ele => {
        ele.addEventListener("input", function () {
            this.value = formatPhoneNumber(this.value);
            validateAll();
        });
    });

    email.addEventListener("input", () => {
        validateAll();
    });

    homePhone.addEventListener("blur", function () {
        removeIncompletePhoneNumber(this);
        const errorElement = homePhone.parentElement.getElementsByTagName("span")[0];
        if (!validatePhoneNumber(this.value) && this.value !== "") {
            errorElement.textContent = "Invalid phone number format. Use (xxx) xxx - xxxx.";
        } else {
            errorElement.textContent = "";
        }
    });

    busPhone.addEventListener("blur", function () {
        removeIncompletePhoneNumber(this);
        validateAll();
        const errorElement = busPhone.parentElement.getElementsByTagName("span")[0];
        if (!validatePhoneNumber(this.value) && this.value !== "") {
            errorElement.textContent = "Invalid phone number format. Use (xxx) xxx - xxxx.";
        } else {
            errorElement.textContent = "";
        }
    });

    email.addEventListener("blur", function () {
        const errorElement = email.parentElement.getElementsByTagName("span")[0];
        if (!validateEmail(this.value)) {
            errorElement.textContent = "Invalid email address.";
        } else {
            errorElement.textContent = "";
        }
    });

    const moveToPassword = document.getElementById("collapseTwo").querySelector(".btn-primary");

    moveToPassword.addEventListener("click", (e) => {
        document.getElementById("collapseThree").classList.add("show");
        document.getElementById("collapseTwo").classList.remove("show");
        document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.add("collapsed");
        if (document.getElementById("headingThree").getElementsByTagName("button")[0].classList.contains("collapsed")) {
            document.getElementById("headingThree").getElementsByTagName("button")[0].classList.remove("collapsed")
        }
    });

    function validateAll() {
        if (validatePhoneNumber(homePhone.value) && validateEmail(email.value)) {
            if (busPhone.value.trim().length > 0 && validatePhoneNumber(busPhone.value)) {
                moveToPassword.classList.remove("d-none");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.remove("disabled");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.remove("collapsed");
            }
            else if (busPhone.value.trim().length == 0) {
                moveToPassword.classList.remove("d-none");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.remove("disabled");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.remove("collapsed");
            } else {
                !moveToPassword.classList.contains("d-none") && moveToPassword.classList.add("d-none");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("disabled");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("collapsed");
            }
        }
        else {
            !moveToPassword.classList.contains("d-none") && moveToPassword.classList.add("d-none");
            document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("disabled");
                document.getElementById("headingThree").getElementsByTagName("button")[0].classList.add("collapsed");
        }
    }



    // ------------ Passwords validation ---------------

    const passwordField = document.getElementById("Password");
    const cnfrmPasswordField = document.getElementById("ConfirmPassword");
    const validationList = document.getElementById("validation-list");
    const lowercase = document.getElementById("lowercase");
    const uppercase = document.getElementById("uppercase");
    const number = document.getElementById("number");
    const special = document.getElementById("special");
    const passLength = document.getElementById("length");
    const messageDiv = document.getElementById('success-messages');
    const completeValidation = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[a-zA-Z\d\W_]{8,16}$/;

    passwordField.addEventListener("focus", () => {
        validationList.style.display = "block";
    });

    // Hide the validation list when the password field loses focus
    passwordField.addEventListener("blur", () => {
        validationList.style.display = "none";
    });

    const validationsChecklist = [
        /[a-z]/,
        /[A-Z]/,
        /\d/,
        /[\W_]/,
        /^[a-zA-Z\d\W_]{8,16}$/,
    ];
    const validationFeilds = [lowercase, uppercase, number, special, passLength];

    passwordField.addEventListener("input", function () {
        console.log(completeValidation.test(passwordField.value), cnfrmPasswordField.value == passwordField.value);

        const value = passwordField.value;

        validationsChecklist.forEach((ele, index) => {
            if (ele.test(value)) {
                validationFeilds[index].classList.add("valid");
                validationFeilds[index].classList.remove("invalid");
                validationFeilds[index]
                    .querySelector("i")
                    .classList.replace("fa-times", "fa-check");
                buttonDisabled = false;
            } else {
                validationFeilds[index].classList.add("invalid");
                validationFeilds[index].classList.remove("valid");
                validationFeilds[index]
                    .querySelector("i")
                    .classList.replace("fa-check", "fa-times");
                buttonDisabled = true;
            }
        });
    });

    [cnfrmPasswordField, passwordField].forEach(ele => {
        ele.addEventListener("input", () => {
            if (passwordField.value === cnfrmPasswordField.value) {
                messageDiv.innerHTML = `<div class="text-success">Password Matches</div>`;
            } else {
                messageDiv.innerHTML = `<div class="text-warning">Passwords Don't Match !!!!!</div>`;
            }
            if (
                passwordField.value.length === 0 &&
                cnfrmPasswordField.value.length === 0
            ) {
                messageDiv.style.display = "none";
            }
        });
    });

    [passwordField, cnfrmPasswordField].forEach(ele => {
        var icon = ele.parentElement.getElementsByTagName('i')[0];
        icon.addEventListener('click', (event) => {
            if (icon.classList.contains('fa-eye')) {
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
                ele.setAttribute('type', 'text');
            } else {
                icon.classList.add('fa-eye');
                icon.classList.remove('fa-eye-slash');
                ele.setAttribute('type', 'password');
            }
        });
    });

    let submitBtn = document.getElementById("moveToProfileBtn");
    console.log("yttuwiygywguw",document.getElementById("userDetails"), document.getElementById("moveToProfileBtn"), submitBtn)

    const allInputs = document.getElementsByTagName("input");

    // Convert HTMLCollection to an array and apply forEach
    for (const ele of allInputs) {
        ele.addEventListener("change", () => {
            console.log("here", !document.getElementById("headingThree").getElementsByTagName("button")[0].classList.contains("disabled"),
                !document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.contains("disabled"),
                completeValidation.test(passwordField.value), cnfrmPasswordField.value == passwordField.value);

            if (!document.getElementById("headingThree").getElementsByTagName("button")[0].classList.contains("disabled") &&
                !document.getElementById("headingTwo").getElementsByTagName("button")[0].classList.contains("disabled") &&
                completeValidation.test(passwordField.value) && cnfrmPasswordField.value == passwordField.value) {
                submitBtn.classList.remove("disabled");
            }
        });
    }
    

    submitBtn.addEventListener("click", function (e) {
        e.preventDefault();
        const loader = document.getElementById("loader");
        loader.style.display = "flex";
        this.classList.add("d-none");
        document.getElementById("userDetails").classList.add("d-none"); 
        setTimeout(function () {
            // Hide the loader
            loader.style.display = "none";
            document.getElementById("sbmtBtn").classList.remove("d-none");
            document.getElementById("imageUploadSection").classList.remove("d-none");
            // Hide the submit button
        }, 2000);
    });

    document.getElementById("ProfileImage").addEventListener("change", function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById("uploadedImage").src = e.target.result;
            };
            reader.readAsDataURL(file);
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

