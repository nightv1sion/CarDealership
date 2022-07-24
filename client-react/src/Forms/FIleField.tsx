import { ChangeEvent } from "react";

export default function FileField(props: fileFieldProps){
    return <>
        <input id={props.description} onChange={props.onChange} type = "file" multiple />
    </>
}

interface fileFieldProps{
    description: string;
    onChange: (event: ChangeEvent<HTMLInputElement>) => void;
}