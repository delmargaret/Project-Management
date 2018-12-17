import React, { Component } from 'react';
import {Button} from 'react-bootstrap';
import '../styles/AdminHomePage.css';
import Menu from './Menu';
import {Grid, Row, Col} from 'react-bootstrap';


class AdminPage extends Component{
      render(){
          return(
            <Grid>
            <Row>
              <Col sm={3}>{<Menu/>}</Col>
              <Col sm={9}>
              <div className="span10" id="homepage">
              <h1>Добро пожаловать!</h1>
                  <div id="buttons">
                    <div className="button" >
                        <Button bsSize="large" href="/staff" id="staffButton">Добавить сотрудника</Button>
                    </div>
                    <div className="button" >
                        <Button bsSize="large" href="/projects" id="projectsButton">Добавить проект</Button>
                    </div>
                  </div>
              </div>
              </Col>
            </Row>
          </Grid>
          );
      };
}

export default AdminPage;