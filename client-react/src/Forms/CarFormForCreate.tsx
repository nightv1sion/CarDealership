import { useFormik } from "formik"
import Field from "./Field";
import * as Yup from "yup";
import { firstLetterCapitalRule } from "../ValidationRules";
import { carForCreateDTO, dealerShopMiniDTO } from "../Interfaces";

export default function CarFormForCreate(props: carFormForCreateProps){
    
    

    const formSchema = Yup.object().shape({
        licencePlates: Yup.string().required(),
        brand: Yup.string().required().test(firstLetterCapitalRule),
        model: Yup.string().required().test(firstLetterCapitalRule),
        country: Yup.string().required().test(firstLetterCapitalRule),
        bodyType: Yup.string().required().test(firstLetterCapitalRule),
        modification: Yup.string().required().test(firstLetterCapitalRule),
        transmission: Yup.string().required().test(firstLetterCapitalRule),
        drive: Yup.string().required().test(firstLetterCapitalRule),
        engineType: Yup.string().required().test(firstLetterCapitalRule),
        color: Yup.string().required().test(firstLetterCapitalRule),
        productionYear: Yup.number().required().test((value) => value ? value > 0 : false),
        numberOfOwners: Yup.number().required().test((value) => value ? value > 0 : false),
        dealerShopOrdinalNumber: Yup.number().required().test((value) => {
            if(props.dealerShops.findIndex( d => d.ordinalNumber == value) == -1)
                return false;
            else 
                return true;
        }),
    });

    let carFormik = {
        licencePlates: "",
        brand: "",
        model: "",
        country: "",
        bodyType: "",
        modification: "",
        transmission: "",
        drive: "",
        engineType: "",
        color: "",
        productionYear: 0,
        numberOfOwners: 0,
        dealerShopOrdinalNumber: 0,
    };

    const postData = (data: carForCreateDTO) => {
        let dealerShop = props.dealerShops.find(d => d.ordinalNumber == data.dealerShopOrdinalNumber);
        if(dealerShop)
        {
            fetch(process.env.REACT_APP_API + "dealershops/" + dealerShop.id + "/cars", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data),
            })
            .then(response => {
                if(response.status == 201)
                    console.log("Car successfully created");
                else 
                    console.log("Creating of car failed");
            })
        }
    };
    
    const formik = useFormik({
        initialValues: carFormik,
        onSubmit: (values) => {
            props.closeForm();
            console.log(JSON.stringify(values));
            postData(values);
        },
        validationSchema: formSchema,
    });
    
    return <>
        <form onSubmit = {formik.handleSubmit}>
            <Field formik = {formik} description="Licence Plates" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Brand" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Model" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Country" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Body Type" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Modification" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Transmission" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Drive" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Engine Type" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Color" errorMessage="is required and the first letter must be capital"/>
            <Field formik = {formik} description="Production Year" errorMessage="is required and must be number"/>
            <Field formik = {formik} description="Number Of Owners" errorMessage="is required and must be number"/>
            <Field formik = {formik} description="Dealer Shop Ordinal Number" errorMessage="is required and must be existing ordinal number"/>
            <div><button className = "btn btn-primary" type="submit" disabled = {!formik.isValid}>Create</button></div>
        </form>
    </>
} 

interface carFormForCreateProps {
    closeForm: Function;
    dealerShops: dealerShopMiniDTO[];
}