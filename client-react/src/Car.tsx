import { resolve } from "node:path/win32";
import { useEffect, useState } from "react";
import { car } from "./Interfaces";
import CarTable from "./Tables/CarTable";

export default function Car(){
    const [cars, setCars] = useState<car[]>([]); 

    const getData = () => {
        fetch(process.env.REACT_APP_API + "car", {
            method: "GET",
            headers: {}
        })
        .then(response => {
            if(response.status==200)    
                return response.json();
            else 
                throw new Error(response.statusText);
        })
        .then(data => setCars(data))
        .catch(error => console.log(error));
    }

    useEffect(() => getData(), []);
    

    return <>
        <div className="text-center"><h3>Cars</h3></div>
        <CarTable cars = {cars} className = />
    </>
}