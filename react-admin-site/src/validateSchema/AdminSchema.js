import * as yup from "yup";


export const adminAddSchema = yup.object().shape({
    userName: yup.string()
                .min(2, 'User name must be at least 2 characters')
                .max(8, 'User name must be 8 characters or less')
                .required("User name field should be required please"),

    firstName: yup.string()
                .min(2, 'First name must be at least 2 characters')
                .max(100, 'First name must be 100 characters or less')
                .required("First name field should be required please"),
    lastName: yup.string()
                .min(2, 'Last name must be at least 2 characters')
                .max(100, 'Last name must be 100 characters or less')
                .required("Last name  field should be required please"),
    password: yup
        .string()
        .max(255, "Password must be at most 255 characters")
        .matches(
            /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^\\._-])(?!.*\s).{8,}$/,
            "Must contain 8 characters, one uppercase, one lowercase, one number and one special case character"
        )
        .required("Please enter your password."),
    confirmPassword: yup
        .string()
        .required("Please enter confirm password.")
        .oneOf(
            [yup.ref("password"), null],
            "Confirm password must match"
        ),
    phone: yup.string().matches(/^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$/, 'Phone number is not valid'),
    email: yup.string()
        .email("Must be a valid email address")
        .max(255, "Must be shorter than 255")
        .required("Must enter an email"),
    birthday: yup.date()
        .required("Birthday should be required")
        .test('is-at-least-18', 'Admin is under 18. Please select a different date', function (value) {
            // Calculate the age in years
            const age = Math.floor((new Date() - new Date(value).getTime()) / 3.15576e+10);
            return age >= 18;
        }),
    registeredDate: yup.date()
        .when(
            "birthday",
            (birthday, schema) => birthday && schema.min(birthday, "Joined date is not later than Date of Birth. Please select a different date"))
        .required("Join day should be required"),

})
export const adminEditSchema = yup.object().shape({
    firstName: yup.string().required("First name field should be required please"),
    lastName: yup.string().required("Last name field should be required please"),
    phone: yup.string().matches(/^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$/, 'Phone number is not valid'),
    email: yup.string()
        .email("Must be a valid email address")
        .max(255, "Must be shorter than 255")
        .required("Must enter an email"),
    birthday: yup.date()
        .test('is-at-least-18', 'Admin is under 18. Please select a different date', function (value) {
            // Calculate the age in years
            const age = Math.floor((new Date() - new Date(value).getTime()) / 3.15576e+10);
            return age >= 18;
        })
        .required("Birthday should be required"),
    registeredDate: yup.date()
        .when(
            "birthday",
            (birthday, schema) => birthday && schema.min(birthday, "Joined date is not later than Date of Birth. Please select a different date"))
        .required("Join day should be required"),
})
