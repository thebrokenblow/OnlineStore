Feature: management a list of product

    Background: The service stores a list of product and provides an API for CRUD operations with them.

    Scenario: We can create several product categories

        Given we want to add several products, but to add products we have to add categories:
        | Name              | Description                                           |
        | Electronics       | Devices and gadgets for everyday use                  |
        | Home Appliances   | Appliances for kitchen and home use                   |

        When we add several products:
        | Name               | Description                                          | Price | CategoryName     |
        | Wireless Mouse     | Ergonomic wireless mouse with 3 buttons              | 24.99 | Electronics      |
        | Portable Speaker   | Compact Bluetooth speaker with 10-hour battery life  | 49.99 | Electronics      |
        | Coffee Maker       | Automatic drip coffee maker with 12-cup capacity     | 59.99 | Home Appliances  |

        Then we can get all records of products:
         | Name               | Price |
         | Wireless Mouse     | 24.99 |
         | Portable Speaker   | 49.99 |
         | Coffee Maker       | 59.99 |

    Scenario: We can create several product and update one

     Given we want to add several products and then update one, but to add products we have to add categories:
        | Name              | Description                                           |
        | Electronics       | Devices and gadgets for everyday use                  |
        | Home Appliances   | Appliances for kitchen and home use                   |

     When we add several products:
        | Name               | Description                                          | Price | CategoryName     |
        | Wireless Mouse     | Ergonomic wireless mouse with 3 buttons              | 24.99 | Electronics      |
        | Portable Speaker   | Compact Bluetooth speaker with 10-hour battery life  | 49.99 | Electronics      |
        | Coffee Maker       | Automatic drip coffee maker with 12-cup capacity     | 59.99 | Home Appliances  |

     And we update first product:
          | Name               | Description                                          | Price | CategoryName     |
          | Smartwatch         | Waterproof smartwatch with heart rate monitor        | 149.99| Electronics      |

     Then we get all products after the update:
          | Name               | Price  |
          | Portable Speaker   | 49.99  |
          | Coffee Maker       | 59.99  |
          | Smartwatch         | 149.99 |

    And we get details updated product