import React from "react";
import { Layout, Menu, Breadcrumb,Statistic, Row, Col,Progress  } from 'antd';
import "antd/dist/antd.css";
import "./config"
class ApiTimer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {data:{}};
    this.host=global.config.host;
  }
  tick() {
    fetch(this.host+"/GetTemp").then(response=>{
        response.json().then(data=>{
            data.cpuload=parseFloat(data.cpuload.slice(0,data.cpuload.indexOf(".")+3));
            data.ramload=parseFloat(data.ramload.slice(0,data.ramload.indexOf(".")+3));
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
            <Statistic title="Cpu Temprature" value={this.state.data.cputemp} />
        </Col>
        <Col span={12}>
            <Statistic title="Gpu Temprature" value={this.state.data.gputemp} />
        </Col>
        </Row>
        <Row  justify="space-around" style={{ marginTop:20 }}>
        <Col span={12}>
            <div style={{ marginBottom:10 }}>Cpu Used</div>
            <Progress type="dashboard" percent={this.state.data.cpuload} />
        </Col>
        <Col span={12}>
            <div style={{ marginBottom:10 }}>Memory Used</div>
            <Progress type="dashboard" percent={this.state.data.ramload} />
        </Col>
        </Row>
    </div>
    );
  }
}
export default ApiTimer;