import React from "react";
import {Statistic, Row, Col,Progress  } from 'antd';
import "antd/dist/antd.css";
import "./config"
class ApiTimer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
        data:{
            diskLoad:[]
        }
    };
    this.host=global.config.host;
  }
  tick() {
    fetch(this.host+"/GetTemp").then(response=>{
        response.json().then(data=>{
            data.cpuLoad=Math.floor(data.cpuLoad*100)/100
            data.ramLoad=Math.floor(data.ramLoad*100)/100
            this.setState({data:data});
        });
    });

  }
 
  componentDidMount() {
    this.interval = setInterval(() => this.tick(), 1000);
  }
 
  componentWillUnmount() {
    clearInterval(this.interval);
  }
 
  render() {
    return (
    <div style={{ background: '#fff', padding: 24, minHeight: 280 }}>
        <Row justify="space-around">
        <Col span={12}>
            <Statistic title="Cpu Temprature" value={this.state.data.cpuTemp} />
        </Col>
        <Col span={12}>
            <Statistic title="Gpu Temprature" value={this.state.data.gpuTemp} />
        </Col>
        </Row>
        <Row  justify="space-around" style={{ marginTop:20 }}>
        <Col span={12}>
            <div style={{ marginBottom:10 }}>Cpu Used</div>
            <Progress type="dashboard" percent={this.state.data.cpuLoad} />
        </Col>
        <Col span={12}>
            <div style={{ marginBottom:10 }}>Memory Used</div>
            <Progress type="dashboard" percent={this.state.data.ramLoad} />
        </Col>
        </Row>
        <Row style={{ marginTop:20 }}>
            <Col span={24}><div style={{ marginBottom:10 }}>Disk Loads</div></Col>
            {
                this.state.data.diskLoad.map((item, index)=>{
                    return (
                        <Col span={24} key={index}>
                            <div>{item.name}</div>
                            <Progress percent={Math.floor(item.load*100)/100} status="active" />
                        </Col>
                    )
                })
            }
        </Row>
    </div>
    );
  }
}
export default ApiTimer;