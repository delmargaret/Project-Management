import React, { Component } from 'react';
import './App.css';
import {
    Route,
    NavLink,
    HashRouter
  } from "react-router-dom";
import AddProjectForm from "./components/AddProject";
import RoleList from "./components/AddEmployee";

class App extends Component {
    render() {
      return (
        <HashRouter>
          <div>
            <ul className="header">
              <li><NavLink to="/">Сотрудники</NavLink></li>
              <li><NavLink to="projects">Проекты</NavLink></li>
            </ul>
            <div className="content" id="content"> 
                <Route exact path="/" component={RoleList}/>
                <Route path="/projects" component={AddProjectForm}/>
            </div>
          </div>
        </HashRouter>  
      );
    }
  }
   
  export default App;
