import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import RoleList from './App';

ReactDOM.render(<RoleList apiUrl="http://localhost:12124/api/Role/GetRoles"/>, document.getElementById('root'));
