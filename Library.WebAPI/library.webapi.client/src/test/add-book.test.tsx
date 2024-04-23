import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom/jest-globals';
import '@testing-library/jest-dom';

import AddBook from '../components/add-book';
import * as BookService from '../api/book';

// Mock de BookService
jest.mock('../api/book', () => ({
    Add: jest.fn().mockResolvedValue({}),
}));


describe('<AddBook />', () => {
  it('renderizar', () => {
    render(<AddBook isAdded={jest.fn()} close={jest.fn()} />);
    const titleInput = screen.getByLabelText(/título/i);
    const authorInput = screen.getByLabelText(/autor/i);
    const addButton = screen.getByRole('button', { name: /agregar/i });

    expect(titleInput).toBeInTheDocument();
    expect(authorInput).toBeInTheDocument();
    expect(addButton).toBeInTheDocument();
  });
  
  it('el botón Agregar = disabled cuando los campos están vacíos', () => {
    render(<AddBook isAdded={jest.fn()} close={jest.fn()} />);
    const addButton = screen.getByRole('button', { name: /agregar/i });
    expect(addButton).toBeDisabled();
  });

  it('Llamar a la función Add con datos correctos cuando se envía el formulario', async () => {
    render(<AddBook isAdded={jest.fn()} close={jest.fn()} />);
    const titleInput = screen.getByLabelText(/título/i);
    const authorInput = screen.getByLabelText(/autor/i);
    const addButton = screen.getByRole('button', { name: /agregar/i });

    fireEvent.change(titleInput, { target: { value: 'Nuevo Libro' } });
    fireEvent.change(authorInput, { target: { value: 'Autor 1' } });

    fireEvent.click(addButton);

    await waitFor(() => {
        expect(BookService.Add).toHaveBeenCalledWith({
        title: 'Nuevo Libro',
        author: 'Autor 1',
        isAvailable: true
      });
    });
  });


});
