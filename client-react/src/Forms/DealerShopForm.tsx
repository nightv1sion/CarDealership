import { Formik, FormikHelpers, useFormik } from "formik";
import { Button, Form } from "react-bootstrap";
import { dealerShop, dealerShopCreationDTO } from "../Interfaces";
import * as Yup from "yup";
import { ChangeEvent, ChangeEventHandler, useState } from "react";
import {uniqueOrdinalNumberRule, firstLetterCapitalRule} from "../ValidationRules";
import Field from "./Field";
import FileField from "./FIleField";
import axios from "axios";
import MapInput from "./MapInput";
import 'leaflet/dist/leaflet.css';
import { MapContainer, Marker, TileLayer } from "react-leaflet";


export default function DealerShopForm(props: dealerShopCreateFormProps){
    
    const checkLocationCoords = (nums: string[]) => {
        return nums.length == 2 && typeof(+nums[0]) == "number" && typeof(+nums[1]) == "number";
    }

    const getLocation = (location: string) => {
        if(checkLocationCoords(location.split(", "))){
            let arr = location.split(", ");
            let pos = [+arr[0], +arr[1]];
            return pos;
        }
        return [13.003725, 55.60487];
    }

    const [files, setFiles] = useState<File[]>(props.shop && props.shop.files ? props.shop.files : []);

    const [fileUrls, setFileUrls] = useState<string[]>(props.shop && props.shop.fileUrls ? props.shop.fileUrls : []);

    const [touchedFileInput, setTouchedFileInput] = useState<boolean>(false);

    const [position, setPosition] = useState<number[]>(props.shop ? getLocation(props.shop.location) : [13.003725, 55.60487]);

    let shop = {
        ordinalNumber: 0,
        country: '',
        city: '',
        address: '',
        email: '',
        phoneNumber: '',
     };
    if(props.shop){
        shop = {
            ordinalNumber: props.shop.ordinalNumber,
            country: props.shop.country,
            city: props.shop.city,
            address: props.shop.address,
            email: props.shop.email,
            phoneNumber: props.shop.phoneNumber,
        }
        
    }

    
    
    const formSchema = Yup.object().shape({
        ordinalNumber: Yup.number().required().test((value) => uniqueOrdinalNumberRule.test(value, props.allOrdinalNumbers)),
        country: Yup.string().required().test(firstLetterCapitalRule),
        city: Yup.string().required().test(firstLetterCapitalRule),
        address: Yup.string().required().test(firstLetterCapitalRule),
        email: Yup.string().required().email(),
        phoneNumber: Yup.string().required(),
        // location: Yup.string().required().test((value) => value ? checkLocationCoords(value.split(" ")) : false),
    });


    const postData = (data: dealerShopCreationDTO) => {
        let formData = new FormData();
        
        for(let [key, value] of Object.entries(data)){
            formData.append(key, value);
        }

        if(files) 
            for(let i = 0; i<files.length; i++)
                formData.append("files", files[i]);

        console.log(formData)

        formData.append("location", position[0] + ", " + position[1]);

        axios({
            method: "POST",
            url: process.env.REACT_APP_API + "dealershop", 
            data: formData,
            headers: {
                contentType: "multipart/form-data"
            }
        })
        .then((response) => {console.log(response); props.getData()});
    };
    
    const handleChange = (event:ChangeEvent<HTMLInputElement>) => {
        let newFilesArray = new Array(...files);
        let newFileUrlsArray = new Array(...fileUrls);
        if(event.target.files)
            for(let i = 0; i<event.target.files.length; i++){
                newFilesArray.push(event.target.files[i]);
                newFileUrlsArray.push(URL.createObjectURL(event.target.files[i]));
            }
        
        setFiles(newFilesArray);
        setFileUrls(newFileUrlsArray);
        setTouchedFileInput(true);
    }

    const updateData = (data: dealerShopCreationDTO) => {

        fetch(process.env.REACT_APP_API + "dealershop",{
            method: "PUT",
            headers: {
                contentType: "application/json"
            },
            body: JSON.stringify(data)
        })
        .then(response => response.json())
        .then((response) => {console.log(response); props.getData()});
    }

    const formik = useFormik({initialValues: 
        shop,
        onSubmit: (values) => {
            console.log(JSON.stringify(values));
            if(props.shop)
                updateData(values);
            else
                postData(values);
            props.closeForm();
        },
        validationSchema: formSchema
    });

    

    const locationChange = (event: ChangeEvent<HTMLInputElement> ) => {
        let nums = event.target.value.split(", ");
        if(nums.length != 2) 
            return;
        if(checkLocationCoords(nums)){
            let array = new Array(+nums[0], +nums[1]);
            setPosition(array);
        }
    }

    const handleDeleteFile = (index: number) => {
        let tempArrayFiles = [...files];
        let tempArrayFileUrl = [...fileUrls];

        tempArrayFiles.splice(index, 1);
        tempArrayFileUrl.splice(index, 1);

        setFileUrls(tempArrayFileUrl);
        setFiles(tempArrayFiles)
    }

    return <>
        <form onSubmit = {formik.handleSubmit} encType="multipart/form-data">
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


            {files && files.length < 1 && touchedFileInput ? <div className = "text-danger">Must be one more photos here</div> : <div>Photos</div>}

            <FileField description="files" onChange={handleChange} accept = ".jpg, .jpeg, .png"/>

            
            
            {fileUrls && fileUrls && fileUrls.map((fileUrl,index) => 
            {
                return <div className = "mt-3" key = {index}><img src={fileUrl} style = {{width: "200px"}}/> <button className = "btn btn-danger" type = "button" onClick = {() => handleDeleteFile(index)}>Delete</button></div> 
            })}
            
            <div className = 'text-center mt-3'>
                <button type="submit" className = "btn btn-dark" disabled={!formik.isValid || files && files.length < 1}>{props.shop ? "Edit" : "Create"}</button>
            </div>

            
        </form>
    </>
}

interface dealerShopCreateFormProps {
    getData: Function;
    closeForm: Function;
    allOrdinalNumbers: number[];
    shop?: dealerShop;
}