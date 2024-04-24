import React from 'react';
import Books from './books';
import { Typography } from '@mui/material';
import User from './user';

const Dashboard: React.FC = () => {
  return (<>
    <Typography variant='h3' sx={{mb: 3}}>
        Biblioteca Digital
    </Typography>

    <User />

    <Books/>
    </>);
};

export default Dashboard;
