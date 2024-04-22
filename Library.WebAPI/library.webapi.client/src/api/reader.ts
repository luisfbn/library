import axios from 'axios';

const BASE_URL = 'http://localhost:5190/api';

const ReaderService = {


  async GetAll() {
    try {
        
        const response = await axios.get(`${BASE_URL}/readers`);
        console.log(response.data);
        return response.data;

    } catch (error) {
        console.error(error);
        throw error;
    }
  },
};

export default ReaderService;
