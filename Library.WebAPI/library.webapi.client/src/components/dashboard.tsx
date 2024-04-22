import React from 'react';
import Books from './books';
import { Typography } from '@mui/material';

const Dashboard: React.FC = () => {
  return (<>
    <Typography variant='h3' sx={{mb: 3}}>
        Biblioteca Digital
    </Typography>

    <Books/>
    </>);
};

export default Dashboard;
