import { act, fireEvent, render, screen, waitFor,  } from '@testing-library/react';
import '@testing-library/jest-dom';

import Books from '../components/books';
import * as BookService from '../api/book';


jest.mock('../api/book', () => ({
  GetAll: jest.fn(),
  Delete: jest.fn(),
}));


describe('<Books />', () => {
  beforeEach(() => {
    (BookService.GetAll as jest.Mock).mockClear();
    (BookService.Delete as jest.Mock).mockClear();
  });

  it('Renderiza', () => {
    render(<Books />);
    expect(screen.getByText('Listado de Libros')).toBeInTheDocument();
  });

  it('Obten libros', async () => {
    const mockBooks = [
      { id: 1, title: 'Book 1', author: 'Author 1', isAvailable: true },
      { id: 2, title: 'Book 2', author: 'Author 2', isAvailable: false },
    ];
    
    (BookService.GetAll as jest.Mock).mockResolvedValue({ data: mockBooks });

    await act(async () => {
      render(<Books />);
    });

    
    await waitFor(() => {
      expect(screen.getByText('Book 1')).toBeInTheDocument();
      expect(screen.getByText('Book 2')).toBeInTheDocument();
    });
  });

  it('Eliminar libro', async () => {
    const booktitle = 'Book X';
    
    const mockBooks = [
      { id: 1, title: booktitle, author: 'Author X', isAvailable: true },
    ];
    
    (BookService.GetAll as jest.Mock).mockResolvedValue({ data: mockBooks });
    (BookService.Delete as jest.Mock).mockResolvedValue(true);

    await act(async () => {
      render(<Books />);
    });
    
    await waitFor(() => {
      expect(screen.getByText(booktitle)).toBeInTheDocument();
    });

    await waitFor(() => {
      const deleteButton = screen.getByTestId('delete-button-1');
      fireEvent.click(deleteButton);
    });

    await waitFor(() => {
      const confirmButton = document.querySelector('.swal2-confirm');
      fireEvent.click(confirmButton!);
    });

    await waitFor(() => {
      expect(BookService.Delete).toHaveBeenCalledWith('1');
    });
    
    await waitFor(() => {
      expect(screen.queryByText(booktitle)).not.toBeInTheDocument();
    });

  });


});

