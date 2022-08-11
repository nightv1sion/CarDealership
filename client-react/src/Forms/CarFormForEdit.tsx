import { useFormik } from "formik"
import Field from "./Field";
import * as Yup from "yup";
import { firstLetterCapitalRule } from "../ValidationRules";
import { car, carForCreateDTO, dealerShopMiniDTO } from "../Interfaces";
import Car from "../Car";

export default function CarFormForEdit(props: carFormForEditProps){

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
    licencePlates: props.carForEdit.licencePlates,
    brand: props.carForEdit.brand,
    model: props.carForEdit.model,
    country: props.carForEdit.country,
    bodyType: props.carForEdit.bodyType,
    modification: props.carForEdit.modification,
    transmission: props.carForEdit.transmission,
    drive: props.carForEdit.drive,
    engineType: props.carForEdit.engineType,
    color: props.carForEdit.color,
    productionYear: props.carForEdit.productionYear,
    numberOfOwners: props.carForEdit.numberOfOwners,
    dealerShopOrdinalNumber: props.carForEdit.dealerShopOrdinalNumber,
};

const PutData = (values: any) => {
    let carId = props.carForEdit.carId;
    let shop = props.dealerShops.find(ds => ds.ordinalNumber == props.carForEdit.dealerShopOrdinalNumber);
    if(!shop)
        return;
    fetch(process.env.REACT_APP_API + "dealershops/" + shop.id + "/cars/" + carId, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(values)
    })
}

const formik = useFormik({
    initialValues: carFormik,
    onSubmit: (values) => {
        props.closeForm();
        console.log(JSON.stringify(values));
        PutData(values);
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
        <div><button className = "btn btn-primary" type="submit" disabled = {!formik.isValid}>Edit</button></div>
    </form>
</>
} 

interface carFormForEditProps{
    closeForm: Function;
    dealerShops: dealerShopMiniDTO[];
    carForEdit: car;
}