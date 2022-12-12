import * as yup from "yup";

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/


export const adminAddSchema = yup.object().shape({
    userName: yup.string().required("Username field should be required please"),
    firstName: yup.string().required("First name field should be required please"),
    lastName: yup.string().required("Last name field should be required please"),
    password: yup
        .string()
        .max(255, "Password must be at most 255 characters")
        // .matches(
        //     /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^\\._-])(?!.*\s).{8,}$/,
        //     "Must contain 8 characters, one uppercase, one lowercase, one number and one special case character"
        // )
        .required("Please enter your password."),
    confirmPassword: yup
        .string()
        .required("Please enter confirm password.")
        .oneOf(
            [yup.ref("password"), null],
            "Confirm password must match"
        ),
    phone: yup.string().matches(phoneRegExp, 'Phone number is not valid'),
    email: yup.string().email().required("Email field should be required"),
    birthday: yup.date().required("Birthday should be required"),
    registeredDate: yup.date().required("Join day should be required"),
})
export const adminEditSchema = yup.object().shape({
    firstName: yup.string().required("First name field should be required please"),
    lastName: yup.string().required("Last name field should be required please"),
    phone: yup.string().matches(phoneRegExp, 'Phone number is not valid'),
    email: yup.string().email().required("Email field should be required"),
    birthday: yup.date().required("Birthday should be required"),
    registeredDate: yup.date().required("Join day should be required"),
})
