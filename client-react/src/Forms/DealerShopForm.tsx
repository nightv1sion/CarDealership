import { Formik, FormikHelpers, useFormik } from "formik";
import { Button, Form } from "react-bootstrap";
import { dealerShop, dealerShopCreationDTO, photoDTO } from "../Interfaces";
import * as Yup from "yup";
import { ChangeEvent, ChangeEventHandler, useEffect, useState } from "react";
import {uniqueOrdinalNumberRule, firstLetterCapitalRule} from "../ValidationRules";
import Field from "./Field";
import FileField from "./FIleField";
import axios from "axios";
import MapInput from "./MapInput";
import 'leaflet/dist/leaflet.css';
import { MapContainer, Marker, TileLayer } from "react-leaflet";
import { format } from "node:path";


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
    
    const getPhotos = (dealerShopId: string) => {
        let photos: photoDTO[]=[];
        fetch(process.env.REACT_APP_API + "photos/dealershop/" + dealerShopId, 
        {
            method: "GET",
            headers: {
            }
        })
        .then(response => {
            if(response.status == 200)
                return response.json();
            else {
                console.log(response.status + response.statusText);
            }
        })
        .then(data => {
            for(let i = 0; i<data.length; i++){
                let photo = {
                    id: data[i].id, 
                    name: data[i].name, 
                    picture: data[i].picture, 
                    pictureFormat: data[i].pictureFormat};
                photos.push(photo);
            }
        });
        console.log("photos:");
        console.log(photos);
        return photos;
    }

    const copyAllPhotosFromProps = () => {
        let photosForReturn:photoDTO[] = [];

        if(props.photos)
            for(let i=0; i<props.photos.length; i++){
                let tempPhoto:photoDTO = {
                    id: props.photos[i].id.slice(),
                    picture: props.photos[i].picture.slice(),
                    pictureFormat: props.photos[i].pictureFormat.slice(),
                    name: props.photos[i].name.slice()
                }
                photosForReturn.push(tempPhoto);
            }
        return photosForReturn
    } 

    const [photos, setPhotos] = useState<photoDTO[]>(props.photos ? props.photos : []);

    const [files, setFiles] = useState<File[]>([]);

    const [fileUrls, setFileUrls] = useState<string[]>([]);

    const [touchedFileInput, setTouchedFileInput] = useState<boolean>(false);

    const [position, setPosition] = useState<number[]>(props.shop ? getLocation(props.shop.location) : [13.003725, 55.60487]);

    const getPhotosForState = () => {
        if(props.shop)
            setPhotos(getPhotos(props.shop.dealerShopId));
    }

    let initialPhotos:photoDTO[] | undefined = undefined;

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
        if(props.photos)
            initialPhotos = copyAllPhotosFromProps();
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

        console.log(formData);
        axios({
            method: "POST",
            url: process.env.REACT_APP_API + "dealershops", 
            data: formData,
            headers: {
                contentType: "multipart/form-data"
            }
        })
        .then((response) => {
            console.log(response); 
            if(props.setStatus)
                if(response.status == 200)
                    props.setStatus(true, "Dealershop was successfully created")
                else 
                    props.setStatus(false, "Creation of dealershop was failed");
            props.getData()
        });
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

    const handleDeletePhoto = (photoId: string) => {
        let newPhotos = photos.slice().filter(p => p.id != photoId);
        if(newPhotos)
            setPhotos(newPhotos);
        console.log(photos);
        
    }
    
    const deleteOldPhotos = () => {
        if(props.photos){
            for(let i = 0; i<props.photos.length; i++)
                if(photos.findIndex(p => initialPhotos && p.id == initialPhotos[i].id) == -1)
                    fetch(process.env.REACT_APP_API + "photos/dealershop/" + props.photos[i].id, 
                    {
                        method: "DELETE",
                        headers: {}
                    })
                    .then(response => {console.log("status of deleting photo: " + response.status); return response.json()})
                    .then(data => console.log(data));
        }
    }

    const updateData = (data: dealerShopCreationDTO) => {

        let formData = new FormData();
        
        for(let [key, value] of Object.entries(data)){
            formData.append(key, value);
        }
        if(props.shop){
            formData.append("DealerShopId", props.shop?.dealerShopId);
        }
        else {
            return;
        }
        if(files) 
            for(let i = 0; i<files.length; i++)
                formData.append("files", files[i]);

        console.log(formData)

        formData.append("location", position[0] + ", " + position[1]);

        axios({
            method: "PUT",
            url: process.env.REACT_APP_API + "dealershops/" + props.shop.dealerShopId, 
            data: formData,
            headers: {
                contentType: "multipart/form-data"
            }
        })
        .then((response) => {
            console.log(response); 
            if(props.setStatus)
                if(response.status == 200)
                    {
                        deleteOldPhotos();
                        props.setStatus(true, "Dealershop was successfully edited");
                    }
                else
                    props.setStatus(false, "Dealershop wasn't edited");
            props.getData()
        });
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
                return <div className = "mt-3" key = {index}>
                    <img src={fileUrl} style = {{width: "200px"}}/> 
                    <button className = "btn btn-danger" type = "button" onClick = {() => handleDeleteFile(index)}>Delete</button>
                </div> 
            })}
            
            {photos.length != 0 ? 
            photos.map((photo, index) => 
             { 
              return <div key = {index} className = "mt-3">
                <img style = {{"width": "200px"}} src = {`data:${photo.pictureFormat};base64,${photo.picture}`}/> 
                <button className = "btn btn-danger" type = "button" onClick = {() => handleDeletePhoto(photo.id)}>Delete</button>
                </div>
            }) : <></>}

            {props.shop ? <div className = 'text-center mt-3'>
                <button type="submit" className = "btn btn-dark" disabled={!formik.isValid}>Edit</button>
            </div> : <div className = 'text-center mt-3'>
                <button type="submit" className = "btn btn-dark" disabled={!formik.isValid || files && (files.length < 1)}>Create</button>
            </div> }

        </form>
    </>
}

interface dealerShopCreateFormProps {
    getData: Function;
    closeForm: Function;
    allOrdinalNumbers: number[];
    shop?: dealerShop;
    setStatus?(status: boolean, message: string) : void;
    photos?: photoDTO[];
}