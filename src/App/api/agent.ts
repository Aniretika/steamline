import axios, { Axios, AxiosResponse } from 'axios';
import { ILine } from '../models/line';

axios.defaults.baseURL = 'https://localhost:7273/api/Line';

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    create: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    update: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    delete: <T> (url: string) => axios.post<T>(url).then(responseBody),
}

export async function GetPositions() {
    return await requests.get<ILine[]>('/Get').then(response =>
        {   
            let positions: ILine[] = [];
            response.forEach(line => {
                positions.push(line);
            })
            return positions;
        })
}

export async function AddPosition(position: ILine) {
    await axios.post<void>(`/CreatePosition`, position);
}

export async function UpdatePosition(queue: ILine[], updatedData: ILine, oldData: ILine) {
    const dataUpdate = [...queue];
    let newAddedElement = dataUpdate[dataUpdate.indexOf(oldData!)];
    newAddedElement = updatedData;
    await axios.post<void>(`/Edit/${oldData.PositionId}`, newAddedElement);
}

export async function DeletePosition(queue: ILine[], oldData: ILine) {
    const dataDelete = [...queue];
    dataDelete.splice(dataDelete.indexOf(oldData), 1);  
    await axios.post<void>(`/DeletePosition/${oldData.PositionId}`);
}