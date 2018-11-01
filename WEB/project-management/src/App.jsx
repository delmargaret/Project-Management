import React, { Component } from 'react';
import './App.css';

class Role extends Component{
 
    constructor(props){
        super(props);
        this.state = {data: props.role};
    }

    render(){
        return <div>
                <p>Id: {this.state.data.id}</p>
                <p>Название роли: {this.state.data.roleName}</p>
        </div>;
    }
}
 
class RoleList extends Component{
 
    constructor(props){
        super(props);
        this.state = { roles: [] };
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ roles: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    render(){ 
        return <div>
                <h2>Список ролей</h2>
                <div>
                    {
                    this.state.roles.map(function(role){                    
                    return <Role key={role.id} role={role}/>
                    })
                    }
                </div>
        </div>;
    }
}

export default RoleList;
