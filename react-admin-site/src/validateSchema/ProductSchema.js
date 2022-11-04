import * as yup from "yup";

export const productSchema = yup.object().shape({
    productName: yup.string().required("Name field should be required please"),
    originalPrice: yup.number()
            .typeError('Price field should be required please').required("Please provide plan cost.")
            .min(0, "Too little")
            .max(5000, 'Very costly!'),
    promotionPercent: yup.number()
        .typeError('Promotion field should be required please').required("Please provide plan cost.")
        .min(0, "Too little")
        .max(100, 'Very costly!'),

    quantity: yup.number()
        .typeError('Quantity field should be required please').required("Please provide plan cost.")
        .min(0, "Too little")
        .max(5000, 'Very costly!'),
    description:  yup.string().required("Description field should be required please"),
    
    // image: yup.mixed().required('A file is required')
    // .test('fileFormat', 'PDF only', (value) => {
    //   console.log(value); return value && ['application/pdf'].includes(value.type);
    // }),
    // password: yup.string().min(4).max(15).password().required(),
    // confirmPassword: yup.string().oneOf([yup.ref("password"),null])
})
