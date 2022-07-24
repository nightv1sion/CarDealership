import { MapContainer, Marker, TileLayer } from "react-leaflet";

export default function MapInput(){
    return <MapContainer center = {[51.505, -0.09]} zoom = {13} scrollWheelZoom = {false}>
        <TileLayer 
        url = "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        <Marker position = {[51.505, -0.09]}>
        </Marker>
    </MapContainer>
}