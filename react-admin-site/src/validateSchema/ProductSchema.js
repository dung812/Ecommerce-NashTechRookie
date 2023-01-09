import * as yup from "yup";

export const productSchema = yup.object().shape({
    productName: yup.string()
    .required("Name field should be required please")
    .min(2, 'Product name must be at least 2 characters')
    .max(1000, 'Must be exactly 1000 digits'),
    originalPrice: yup.number()
            .typeError('Price field should be required please').required("Please provide plan cost.")
            .min(0, "Too little")
            .max(5000, 'Very costly!'),
    promotionPercent: yup.number()
        .typeError('Promotion field should be required please').required("Please provide plan cost.")
        .min(0, "Invalid percentage field")
        .max(100, 'Very high!'),

    quantity: yup.number()
        .typeError('Quantity field should be required please').required("Please provide plan cost.")
        .min(0, "Invalid quantity field")
        .max(10000, 'Very high!'),
    
    // image: yup.mixed().required('A file is required')
    // .test('fileFormat', 'PDF only', (value) => {
    //   console.log(value); return value && ['application/pdf'].includes(value.type);
    // }),
    // password: yup.string().min(4).max(15).password().required(),
    // confirmPassword: yup.string().oneOf([yup.ref("password"),null])
})
