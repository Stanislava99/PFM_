import { ProSidebar, Menu, MenuItem, SubMenu, SidebarHeader } from 'react-pro-sidebar';
import 'react-pro-sidebar/dist/css/styles.css';
import React, { Component } from 'react';

class SidebarComponent extends Component {
  render() {
    return (
      <ProSidebar>
        <SidebarHeader>
          <h1>PFM</h1>
        </SidebarHeader>
        <Menu iconShape="square">
          <MenuItem >Dashboard</MenuItem>
          <SubMenu title="Transaction">
            <MenuItem>All Transactions</MenuItem>
            <MenuItem>Import Transactions</MenuItem>
          </SubMenu>
          <SubMenu title="Components">
            <MenuItem>Component 1</MenuItem>
            <MenuItem>Component 2</MenuItem>
          </SubMenu>
        </Menu>
    </ProSidebar>
    );
  }
}

export default SidebarComponent;