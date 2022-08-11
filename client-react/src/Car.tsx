import { resolve } from "node:path/win32";
import { ChangeEventHandler, useEffect, useState } from "react";
import { Button, Col, Container, Modal, Row, SplitButton } from "react-bootstrap";
import Select, { MultiValue } from 'react-select'
import CarFormForCreate from "./Forms/CarFormForCreate";

import { car, dealerShopMiniDTO } from "./Interfaces";
import CarTable from "./Tables/CarTable";

export default function Car(){
    const [cars, setCars] = useState<car[]>([]); 
    
    const [dealerShops, setDealerShops] = useState<dealerShopMiniDTO[]>([]);

    const [options, setOptions] = useState<any[]>([]);

    const [chosenOptions, setChosenOptions] = useState<string[]>([]);

    const [isFormOpened, setIsFormOpened] = useState<boolean>(false);

    const dealerShopsToOptions = (arr: dealerShopMiniDTO[]) => {
        let result = [];
        for(let i = 0; i<arr.length; i++)
        {
            if(arr[i])
                result.push({value: arr[i].id.slice(), label: arr[i].ordinalNumber});
        }
        console.log("options:");
        console.log(result);
        return result;
    }

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
            if(data && data.length > 0)
            {
                setDealerShops(data);
                setOptions(dealerShopsToOptions(data));
            }
        })
        .catch(error => console.log(error));
    }

    const getData = (ids: string[]) => {
        console.log(ids);
        if(ids == null || ids.length < 1)
        {
            setCars([]);
            return;
        }
        let allCars:car[] = [];
        let queryIds:string = "(";
        for(let i = 0; i<ids.length; i++){
            queryIds = queryIds.concat(ids[i]);
            if(i+1 < ids.length)
                queryIds = queryIds.concat(",");
        }
        queryIds = queryIds.concat(")");
        console.log(queryIds);
        fetch(process.env.REACT_APP_API + "dealershops/collection/" + queryIds + "/cars", {
            method: "GET",
            headers: {}
        })
        .then(response => {
            if(response.status==200)    
                return response.json();
            else 
                throw new Error(response.statusText);
        })
        .then((data:car[]) => 
            { 
                allCars.push(...copyArrayOfDataFromFetch(data));
                setCars(allCars);
                console.log(allCars);
            })
        .catch(error => console.log(error));
    }
    
    const copyArrayOfDataFromFetch = (data: car[]) => {
        let result = [];
        result.push(...data);
        return result;
    }

    // const handleChangeSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
    //     console.log(event.target.value);
    //     if(dealerShops)
    //         {
    //             let ds = dealerShops.find(d => d.ordinalNumber.toString() == event.target.value);
    //             if(ds)
    //                 getData(ds.id)
    //             else 
    //                 setCars([]);
    //         }
    // }

    const selectOnChange = (newValue: MultiValue<any>) => {
        setChosenOptions(newValue.map(v => v.value));
        getData(newValue.map(v => v.value));
    }

    const handleClose = () => {
        setIsFormOpened(false);
    }

    const handleOpen = () => {
        setIsFormOpened(true);
    }

    useEffect(() => {
        getDealerShops();
    }, []);

    return <>
        <div className="text-center"><h3>Cars</h3></div>
        <div className = "w-50 m-auto mb-3">
            <Container>
                <Row>
                    <Col sm = {10}>
                        <Select className = "w-80" options = {options} isMulti onChange={selectOnChange} placeholder = "Select dealer shops for search cars"/>
                    </Col>
                    <Col sm={2}>
                        <Button onClick={handleOpen}>Create a Car</Button>
                        <Modal className = "modal-md" show={isFormOpened} onHide = {handleClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Create Car Form</Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <CarFormForCreate closeForm = {handleClose} dealerShops = {dealerShops} />
                            </Modal.Body>
                        </Modal>
                    </Col>
                </Row>
            </Container>
            </div>
        <CarTable cars = {cars} dealerShops = {dealerShops} getAllCars = {() => getData(chosenOptions)}/>
    </>
}