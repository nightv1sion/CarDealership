export const firstLetterCapitalRule = {
    name: "capital-first-letter-rule", 
    message: "The first letter must be capital",
    test: (value: string | undefined) => value?.substring(0,1) === value?.substring(0,1).toLocaleUpperCase()
}

export const uniqueOrdinalNumberRule = {
    name: "unique-ordinal-number-rule",
    message: "The ordinal number must be unique",
    test: (value: number | undefined, array: number[]) => 
    value ? !array.includes(value) : false
};
