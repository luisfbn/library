import axios from 'axios';

const BASE_URL = 'http://localhost:5190/api'; 

const LoanService = {
  async RegisterLoan(bookId: number, readerId: number) {
    try {
        const response = await axios.post(`${BASE_URL}/loan/register`, {
        bookId,
        readerId,
        });
        return response.data;

    } catch (error) {
        console.error('Error registering loan:', error);
        throw error;
    }
  },

  async ReturnBook(bookId: number) {
    try {
        const response = await axios.post(`${BASE_URL}/loan/return/${bookId}`);
        return response.data;
    } catch (error) {
        console.error('Error returning book:', error);
        throw error;
    }
  },
};

export default LoanService;