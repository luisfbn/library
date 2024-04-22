import axios from 'axios';

const BASE_URL = 'http://localhost:5190/api';

const BookService = {

  async AddBook(bookData: any) {
    try {
      
        const response = await axios.post(`${BASE_URL}/books`, bookData);
        return response.data;

    } catch (error) {
        console.error(error);
        throw error;
    }
  },

  async DeleteBook(bookId: string) {
    try {

        const response = await axios.delete(`${BASE_URL}/books/${bookId}`);
        return response.data;

    } catch (error) {
        console.error(error);
        throw error;
    }
  },

  async GetAllBooks() {
    try {

        const response = await axios.get(`${BASE_URL}/books`);
        return response.data;

    } catch (error) {
        console.error(error);
        throw error;
    }
  },
};

export default BookService;
