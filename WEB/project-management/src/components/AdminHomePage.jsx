import React, { Component } from 'react';
import {Button} from 'react-bootstrap';
import './AdminHomePage.css';

class AdminHomePage extends Component{

      render(){
          return(
              <div id="homepage">
                  <h1>Добро пожаловать!</h1>
                  <div id="buttons">
                    <div className="button" id="staffButton">
                        <Button bsSize="large" href="/staff">Добавить сотрудника</Button>
                    </div>
                    <div className="button" id="projectsButton">
                        <Button bsSize="large" href="/projects">Добавить проект</Button>
                    </div>
                  </div>
              </div>
          );
      };
}

export default AdminHomePage;
