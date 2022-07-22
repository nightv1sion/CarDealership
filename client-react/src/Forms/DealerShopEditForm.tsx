import { useFormik } from "formik";
import { dealerShop } from "../Interfaces"
import * as Yup from 'yup';

export default function DealerShopEditForm(props: dealerShopEditFormProps){
    const firstLetterCapitalRule = {
        name: "capital-first-letter-rule", 
        message: "The first letter must be capital",
        test: (value: string | undefined) => value?.substring(0,1) === value?.substring(0,1).toLocaleUpperCase()
    }

    const checkUniqueNumber = (value: number | undefined) => 
        value ? !props.allOrdinalNumbers.includes(value) || value == props.shop.ordinalNumber : false
    const uniqueOrdinalNumberRule = {
        name: "unique-ordinal-number-rule",
        message: "The ordinal number must be unique",
        test: checkUniqueNumber
    };

    const formSchema = Yup.object().shape({
        ordinalNumber: Yup.number().required().test(uniqueOrdinalNumberRule),
        country: Yup.string().required().test(firstLetterCapitalRule),
        city: Yup.string().required().test(firstLetterCapitalRule),
        address: Yup.string().required().test(firstLetterCapitalRule)
    });

    
    const formik = useFormik({
        initialValues: {...props.shop},
        onSubmit: values => {
            const uri = process.env.REACT_APP_API + "dealershop";
            fetch(uri, {
                method: "PUT",
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify(values)
            })
            .then(response => response.json())
            .then((data) => {
                console.log(data);
                props.getData();
                props.closeForm();
            });
        },
        validationSchema: formSchema
    }); 
    return <>
        <form onSubmit = {formik.handleSubmit}>
            {formik.touched.ordinalNumber && formik.errors.ordinalNumber ? 
            (<div className = 'text-danger mb-1'>Ordinal Number is required and must be unique number</div>) : 
            <label htmlFor ="ordinalNumber" className = "mb-1">Ordinal Number</label>}
            
            <input className = "form-control" id="ordinalNumber" name = "ordinalNumber" 
            value = {formik.values.ordinalNumber} onChange = {formik.handleChange} onBlur={formik.handleBlur}/>

            {formik.touched.country && formik.errors.country ? 
            (<div className = "text-danger mt-3">Country is required and must have capital first letter</div>)
            : <label htmlFor = "country" className = "mt-3">Country</label>
            }
            <input className = "form-control" id = "country" name="country" 
            value = {formik.values.country} onChange = {formik.handleChange} onBlur = {formik.handleBlur}/>

            {formik.touched.city && formik.errors.city ? 
            <div className = "text-danger mt-3">City is required and must have capital first letter</div>
            : <label htmlFor = "city" className = "mt-3">City</label>}
            <input className = "form-control" id = "city" name = "city" 
            value = {formik.values.city} onChange = {formik.handleChange} onBlur = {formik.handleBlur}/>


            {formik.touched.address && formik.errors.address ? 
            <div className = "text-danger mt-3">Address is required and must have capital first letter</div>
            : <label htmlFor = "address" className = "mt-3">Address</label> }
            <input className = "form-control" id = "address" name = "address" 
            value = {formik.values.address} onChange = {formik.handleChange} onBlur = {formik.handleBlur}/>

            <div className = 'text-center mt-3'>
                <button type="submit" className = "btn btn-dark" disabled={!formik.isValid}>Edit</button>
            </div>
        </form>
    </>
}

interface dealerShopEditFormProps {
    shop: dealerShop;
    getData: Function;
    closeForm: Function;
    allOrdinalNumbers: number[];
}