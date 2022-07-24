export default function Field(props: fieldProps){
    let fieldName = props.description.split(" ").join("");
    fieldName = fieldName.substring(0, 1).toLowerCase() + fieldName.substring(1);
    return <>
        {props.formik.touched[fieldName] && props.formik.errors[fieldName] ? 
            (<div className = 'text-danger mb-1'>{props.description} {props.errorMessage}</div>) : 
            <label htmlFor ={fieldName} className = "mb-1">{props.description}</label>}
            
            <input type = {props.inputType ? props.inputType : "text"} className = "form-control" id={fieldName} name = {fieldName} 
            value = {props.formik.values[fieldName]} onChange = {props.formik.handleChange} onBlur={props.formik.handleBlur}/>
    </>
}

interface fieldProps {
    formik: any;
    description: string;
    inputType?: string;
    errorMessage: string;
}