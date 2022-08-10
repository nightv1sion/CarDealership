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
    licencePlates: string;
    brand: string;
    model: string;
    country: string;
    bodyType: string;
    modification: string;
    transmission: string;
    drive: string;
    engineType: string;
    color: string;
    productionYear: number;
    numberOfOwners: number;
    dealerShopOrdinalNumber: number;
}

export interface carForCreateDTO {
    licencePlates: string;
    brand: string;
    model: string;
    country: string;
    bodyType: string;
    modification: string;
    transmission: string;
    drive: string;
    engineType: string;
    color: string;
    productionYear: number;
    numberOfOwners: number;
    dealerShopOrdinalNumber: number;
}

export interface photoDTO {
    id: string;
    name: string;
    picture: string;
    pictureFormat: string;
}

export interface dealerShopMiniDTO {
    id: string;
    ordinalNumber: number;
}