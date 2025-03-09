Feature: management a list of product categories

    Background: The service stores a list of product categories and provides an API for CRUD operations with them.

    Scenario: We can create several product categories
        When we add product categories:
          | Name               | Description                                                                                            |
          | Electronics        | Devices like smartphones, laptops, and smart home gadgets that enhance connectivity and entertainment. |
          | Home Appliances    | Essential household items such as refrigerators, ovens, and washing machines that improve daily tasks. |
          | Health and Beauty  | Personal care products, including cosmetics and skincare, that promote well-being and appearance.      |   

        Then we get list of product categories:
          | Name               |
          | Electronics        |
          | Home Appliances    |
          | Health and Beauty  |
    
    
    Scenario: We can create several product categories and update one
        Given we add product categories:
          | Name               | Description                                                                                              |
          | Fitness Equipment  | Equipment such as treadmills, weights, and yoga mats designed to support physical exercise and wellness. |
          | Office Supplies    | Items like pens, notebooks, and printers that facilitate productivity and organization in workspaces.    |

        When we update product categories:
          | Name               | Description                                                                                            |
          | Gardening Tools    | Equipment such as shovels, rakes, and pruners used for planting, maintaining, and landscaping gardens. |

        Then we get list of product categories:
          | Name               | Description                                                                                            |
          | Gardening Tools    | Equipment such as shovels, rakes, and pruners used for planting, maintaining, and landscaping gardens. |
          | Office Supplies    | Items like pens, notebooks, and printers that facilitate productivity and organization in workspaces.  |

    Scenario: We can create several product categories and delete one
        Given we add product categories:
          | Name               | Description                                                                                            |
          | Electronics        | Devices like smartphones, laptops, and smart home gadgets that enhance connectivity and entertainment. |
          | Home Appliances    | Essential household items such as refrigerators, ovens, and washing machines that improve daily tasks. |
          | Health and Beauty  | Personal care products, including cosmetics and skincare, that promote well-being and appearance.      | 

        When we delete product category with name: "Electronics"

        Then we get list of product categories:
          | Name               | Description                                                                                            |
          | Home Appliances    | Essential household items such as refrigerators, ovens, and washing machines that improve daily tasks. |
          | Health and Beauty  | Personal care products, including cosmetics and skincare, that promote well-being and appearance.      | 

    @negative
    Scenario: You cannot add a product category with an empty name
        When we add product categories:
          | Name               | Description                                                                                            |
          |                    | Devices like smartphones, laptops, and smart home gadgets that enhance connectivity and entertainment. | 

        Then we get a validation error