import React, { Component } from 'react';
import {Table} from 'react-bootstrap';


class ScheduleDayList extends Component{
    render(){ 
        return <div id="scrollschedule">
            <Table>
            <thead>
                <tr>
                <th>Дни</th>
                </tr>
            </thead>
                        <tbody>
                        {
                    this.props.days.map((day) => { 
                        var dayId = day;
                        var dayName = "";
                        if(dayId==="1"){dayName="Понедельник";}
                        if(dayId==="2"){dayName="Вторник";}
                        if(dayId==="3"){dayName="Среда";}
                        if(dayId==="4"){dayName="Четверг";}
                        if(dayId==="5"){dayName="Пятница";}
                        if(dayId==="6"){dayName="Суббота";}
                        return <tr key={dayId}>
                            <td>{dayName}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                </Table>
        </div>
            }
}

export default ScheduleDayList;