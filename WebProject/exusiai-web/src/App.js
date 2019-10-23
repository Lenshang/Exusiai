import React from "react";
import ReactDOM from "react-dom";
import { Layout, Menu, Breadcrumb,Statistic, Row, Col,Progress  } from 'antd';
import "antd/dist/antd.css";
import ApiTimer from "./ApiTimer"
const { Header, Content, Footer } = Layout;
function App(){
    return(  
      <Layout className="layout">
      <Header>
        <div className="logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['2']}
          style={{ lineHeight: '64px' }}
        >
        </Menu>
        <h1 style={{color:"#fff"}}>Exusiai</h1>
      </Header>
      <Content style={{ padding: '0 50px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>ComputerMonitor</Breadcrumb.Item>
          <Breadcrumb.Item>Index</Breadcrumb.Item>
        </Breadcrumb>
        <ApiTimer></ApiTimer>
      </Content>
      <Footer style={{ textAlign: 'center' }}>Exusiai</Footer>
    </Layout>
  )
}
export default App;
