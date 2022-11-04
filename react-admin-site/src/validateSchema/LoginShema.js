import * as yup from "yup";

export const loginSchema = yup.object().shape({
    UserName: yup.string().required("Username field should be required please"),
    Password: yup
                .string()
                .required('No password provided.') 
                // .min(8, 'Password is too short - should be 8 chars minimum.')
                // .matches(/[a-zA-Z]/, 'Password can only contain Latin letters.')
})