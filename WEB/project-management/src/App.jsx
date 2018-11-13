import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import AddProjectPage from "./components/AddProject";
import RoleList from "./components/AddEmployee";
import ProjectManagerPage from "./components/ProjectManager";
import Header from "./components/Header";
import Menu from "./components/Menu";
import AdminHomePage from "./components/AdminHomePage";
import {Grid, Row, Col} from 'react-bootstrap';

class App extends Component {
    render() {
      return (
        <Router>
                <div>
                    <Header />
                    <Grid>
                      <Row>
                        <Col xs={3} md={2}>{<Menu/>}</Col>
                        <Col xs={15} md={10}>
                        <Switch>
                          <Route exact path="/" component={AdminHomePage} />
                          <Route path="/staff" component={RoleList} />
                          <Route path="/projects" component={AddProjectPage} />
                          <Route path="/manager" component={ProjectManagerPage} />
                        </Switch>
                        </Col>
                      </Row>
                    </Grid>
                </div>
        </Router> 
      );
    }
  }
   
  export default App;
