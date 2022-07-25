import { ChangeEvent } from "react";

export default function FileField(props: fileFieldProps){
    return <>
        <input className = "form-control" id={props.description} onChange={props.onChange} type = "file" multiple accept = {props.accept} title = "Add files"/>
    </>
}

interface fileFieldProps{
    description: string;
    onChange: (event: ChangeEvent<HTMLInputElement>) => void;
    accept: string;
}