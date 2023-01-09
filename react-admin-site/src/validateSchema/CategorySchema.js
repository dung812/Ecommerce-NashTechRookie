import * as yup from "yup";

export const categorySchema = yup.object().shape({
    name: yup.string().max(1000, 'Must be exactly 1000 digits').required("Name field should be required please"),
})