import { useFormik } from "formik";
import { dealerShop } from "../Interfaces"
import * as Yup from 'yup';
import {uniqueOrdinalNumberRule, firstLetterCapitalRule} from "../ValidationRules"
import Field from "../Forms/Field";

export default function DealerShopEditForm(props: dealerShopEditFormProps){
    
    const formSchema = Yup.object().shape({
        ordinalNumber: Yup.number().required().test((value) => uniqueOrdinalNumberRule.test(value, props.allOrdinalNumbers)),
        country: Yup.string().required().test(firstLetterCapitalRule),
        city: Yup.string().required().test(firstLetterCapitalRule),
        address: Yup.string().required().test(firstLetterCapitalRule),
        email: Yup.string().required().email(),
        phonenumber: Yup.string().required(),
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
            
                {/* <Field formik = {formik} description="Ordinal Number"/>

                <Field formik = {formik} description = "Country" />

                <Field formik = {formik} description = "City"/>

                <Field formik = {formik} description = "Address"/> */}

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