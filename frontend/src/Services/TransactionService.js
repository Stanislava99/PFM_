import axios from 'axios'

class TransactionService {
    getTransactions(){
        return axios.get("https://run.mocky.io/v3/883327c8-6d8c-4c40-bd09-10dc2e6c9c10");
    }    
}

export default new TransactionService();