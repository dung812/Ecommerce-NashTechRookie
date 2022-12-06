// HTML Element
const progress = document.querySelector(".progress");
const stepItems = document.querySelectorAll(".step-items");
const multiStepForm = document.querySelector("[data-multi-step]")
const formSteps = [...multiStepForm.querySelectorAll("[data-step]")]

// Input value
const passwordInput = document.querySelector("input[name='NewPassword']");
const confirmPasswordInput = document.querySelector("input[name='confirm-password']");
const firstNameInput = document.querySelector("input[name='FirstName']");
const lastNameInput = document.querySelector("input[name='LastName']");
const avatarInput = document.querySelector("input[name='objFile']");


let currentStep = formSteps.findIndex(step => {
    return step.classList.contains("active")
})

if (currentStep < 0) {
    currentStep = 0
    showCurrentStep()
}

multiStepForm.addEventListener("click", e => {
    let incrementor
    if (e.target.matches("[data-next]")) {
        incrementor = 1
    } else if (e.target.matches("[data-previous]")) {
        incrementor = -1
    }

    if (incrementor == null) return

    const inputs = [...formSteps[currentStep].querySelectorAll("input")]
    const allValid = inputs.every(input => input.reportValidity())
    if (allValid) {

        //Validate password
        if (passwordInput.value.length < 6) {
            alert("Password must contain at least 6 characters");
            return;
        }

        if (passwordInput.value !== confirmPasswordInput.value) {
            alert("Password & Confirm password is not match");
            return;
        }

        currentStep += incrementor

        //
        stepItems[currentStep].classList.add("active");
        const width = Number(progress.style.width.replace("%", "")) + 50;
        progress.style.width = `${width}%`;

        showCurrentStep()
    }
})

formSteps.forEach(step => {
    step.addEventListener("animationend", e => {
        formSteps[currentStep].classList.remove("hide")
        e.target.classList.toggle("hide", !e.target.classList.contains("active"))
    })
})

function showCurrentStep() {
    formSteps.forEach((step, index) => {
        step.classList.toggle("active", index === currentStep)
    })
}

// Preview avatar
function HandlePreviewAvatar(event) {
    const [file] = event.target.files
    if (file) {
        document.querySelector("#avatar-img").src = URL.createObjectURL(file)
    }
}
var formAvatar = document.querySelector("#change-avatar");
formAvatar && formAvatar.addEventListener("submit", function (e) {
    e.preventDefault();

    if (!this.elements["objFile"].value) {
        alert("Please choose your avatar!")
    }
    else {
        this.submit();
    }
})

// Handle submit
const form = document.querySelector("#form-info");
form.addEventListener('submit', (e) => {
    e.preventDefault();

    if (firstNameInput.value.length == 0) {
        alert("First name field is being blank");
        return;
    }
    if (lastNameInput.value.length == 0) {
        alert("First name field is being blank");
        return;
    }

    const btnSubmit = document.querySelector(".btn-submit");
    btnSubmit.classList.add("is-loading")

    if (avatarInput.value.length > 0) {
        console.log("save avatar")

        var formData = new FormData();
        var totalFiles = document.getElementById("objFile").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("objFile").files[i];
            formData.append("objFile", file);
        }
        $.ajax({
            type: "POST",
            url: '/Home/ChangeAvatar',
            data: formData,
            dataType: 'json',
            contentType: false, // Not to set any content heade
            processData: false, // Not to process data
            success: function (data) {
                if (data.status == true) {
                    form.submit();
                } else {
                    alert("upload image fail");
                }
                return;
            },
            error: function (error) {
                alert(JSON.stringify(error));
            }
        });

    }
    else 
        form.submit();

})