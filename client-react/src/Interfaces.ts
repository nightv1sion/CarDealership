export interface dealerShop {
    dealerShopId: string;
    address: string;
    country: string;
    city: string;
    ordinalNumber: number;
    location: string;
    email:string;
    phoneNumber: string;
    files: File[];
    fileUrls: string[];
}
export interface dealerShopCreationDTO{
    address: string;
    country: string;
    city: string;
    ordinalNumber: number;
}

export interface car {
    carId: string;
    brand: string;
    model: string;
    country: string;
}