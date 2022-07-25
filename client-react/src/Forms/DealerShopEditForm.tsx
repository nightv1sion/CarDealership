import { useFormik } from "formik";
import { dealerShop } from "../Interfaces"
import * as Yup from 'yup';
import {uniqueOrdinalNumberRule, firstLetterCapitalRule} from "../ValidationRules"
import Field from "../Forms/Field";
import { MapContainer, Marker, TileLayer } from "react-leaflet";
import { useState } from "react";

export default function DealerShopEditForm(props: dealerShopEditFormProps){
    
    const formSchema = Yup.object().shape({
        ordinalNumber: Yup.number().required().test((value) => uniqueOrdinalNumberRule.test(value, props.allOrdinalNumbers)),
        country: Yup.string().required().test(firstLetterCapitalRule),
        city: Yup.string().required().test(firstLetterCapitalRule),
        address: Yup.string().required().test(firstLetterCapitalRule),
        email: Yup.string().required().email(),
        phonenumber: Yup.string().required(),
    });

    const [position, setPosition] = useState<number[]>([])
    
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
        {/* <form onSubmit = {formik.handleSubmit}>
            
            <Field formik = {formik} description="Ordinal Number" errorMessage="is required and must be unique number"/>

            <Field formik = {formik} description = "Country" errorMessage = "is required and the first letter must be capital"/>

            <Field formik = {formik} description = "City" errorMessage = "is required and the first letter must be capital"/>

            <Field formik = {formik} description = "Address" errorMessage = "is required and the first letter must be capital"/>

            <Field formik = {formik} description = "Email" inputType="email" errorMessage="is required and must be an email"/>

            <Field formik = {formik} description = "Phone Number" errorMessage = "is required" />

            <div>Location</div>
            <input className = "form-control" type="text" onChange = {locationChange} value = {position[0] + ", " + position[1]}/>
            <div className = "mt-3">
            <MapContainer center = {[position[0], position[1]]} zoom = {13} scrollWheelZoom = {false} >
                <TileLayer 
                url = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                <Marker position = {[position[0], position[1]]}>
                </Marker>
            </MapContainer>
            </div>


            <div className = 'text-center mt-3'>
                <button type="submit" className = "btn btn-dark" disabled={!formik.isValid}>Edit</button>
            </div>
        </form> */}
    </>
}

interface dealerShopEditFormProps {
    shop: dealerShop;
    getData: Function;
    closeForm: Function;
    allOrdinalNumbers: number[];
}