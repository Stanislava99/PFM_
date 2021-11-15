import axios from 'axios'

class CostumerService {
    getCustomersLarge(){
        return axios.get("https://run.mocky.io/v3/dafda80d-5e82-4ee5-9e9b-bf8b85d35ecb");
    }    
}

export default new CostumerService();