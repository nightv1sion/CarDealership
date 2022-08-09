import { resolve } from "node:path/win32";
import { ChangeEventHandler, useEffect, useState } from "react";
import { car } from "./Interfaces";
import CarTable from "./Tables/CarTable";

export default function Car(){
    const [cars, setCars] = useState<car[]>([]); 
    
    const [dealerShops, setDealerShops] = useState<any[]>();

    const getDealerShops = () => {
        fetch(process.env.REACT_APP_API + "dealershops/ordinalnumbers", {
            method: "GET",
            headers: {}
        })
        .then(response => {
            if(response.status == 200)
                return response.json();
            else 
                throw new Error(response.statusText);
        })
        .then(data => {
            console.log("ordinalnumbers:")
            console.log(data);
            setDealerShops(data);
        })
        .catch(error => console.log(error));
    }

    const getData = (id: string) => {
        fetch(process.env.REACT_APP_API + "dealershops/" + id + "/cars", {
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
    
    const handleChangeSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
        console.log(event.target.value);
        if(dealerShops)
            {
                let ds = dealerShops.find(d => d.ordinalNumber.toString() == event.target.value);
                if(ds)
                    getData(ds.id)
                else 
                    setCars([]);
            }
    }
    useEffect(() => {
        getDealerShops();
    }, []);

    return <>
        <div className="text-center"><h3>Cars</h3></div>
        <div className = "w-50 m-auto mb-3">
            <input defaultValue = "" type="number" className = "form-control" placeholder = "Search Cars by dealer shop ordinal number" onChange = {handleChangeSearch}/>
            </div>
        <CarTable cars = {cars}/>
    </>
}